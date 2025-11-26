using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using TaskAwait.Library;
using TaskAwait.Shared;

namespace ParallelUI.Web.Controllers;

public class PeopleController : Controller
{
    PersonReader reader = new();

    // OPTION 1: Await (runs sequentially)
    public async Task<IActionResult> WithAwait()
    {
        ViewData["Title"] = "Using async/await (not parallel)";
        ViewData["RequestStart"] = DateTime.Now;
        try
        {
            List<int> ids = await reader.GetIdsAsync();
            List<Person> people = new();

            foreach (int id in ids)
            {
                var person = await reader.GetPersonAsync(id);
                people.Add(person);
            }

            return View("Index", people);
        }
        catch (Exception ex)
        {
            List<Exception> errors = new() { ex };
            return View("Error", errors);
        }
        finally
        {
            ViewData["RequestEnd"] = DateTime.Now;
        }
    }

    public async Task<IActionResult> WithTask()
    {
        ViewData["Title"] = "Using Parallel";
        ViewData["RequestStart"] = DateTime.Now;
        try
        {
            List<int> ids = await reader.GetIdsAsync();
            List<Task<Person>> tasks = new List<Task<Person>>();
            List<Person> people = new();

            foreach (int id in ids)
            {
                var task = reader.GetPersonAsync(id);
                tasks.Add(task);
            }
            var results = await Task.WhenAll(tasks);
            people.AddRange(results);

            return View("Index", people);
        }
        catch (Exception ex)
        {
            List<Exception> errors = new() { ex };
            return View("Error", errors);
        }
        finally
        {
            ViewData["RequestEnd"] = DateTime.Now;
        }
    }

    // OPTION 3: Parallel.ForEachAsync (runs parallel)
    public async Task<IActionResult> WithForEach()
    {
        ViewData["Title"] = "Using Task";
        ViewData["RequestStart"] = DateTime.Now;
        try
        {
            List<int> ids = await reader.GetIdsAsync();
            ConcurrentBag<Person> people = new();

            await Parallel.ForEachAsync(ids, async (id, cancelToken) =>
            {
                var person = await reader.GetPersonAsync(id, cancelToken);
                people.Add(person);
            });
            return View("Index", people);
        }
        catch (Exception ex)
        {
            List<Exception> errors = new() { ex };
            return View("Error", errors);
        }
        finally
        {
            ViewData["RequestEnd"] = DateTime.Now;
        }
    }
}

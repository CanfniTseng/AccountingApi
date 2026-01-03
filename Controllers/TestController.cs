using AccountingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    // 1. 查詢所有帳目 (包含類別名稱)
    [HttpGet]
    public IActionResult GetAll()
    {
        var list = _context.Transactions
                           .Include(t => t.Category) // 這就是 SQL 的 JOIN
                           .ToList();
        return Ok(list);
    }

    // 2. 新增帳目
    [HttpPost]
    public IActionResult Create(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        _context.SaveChanges();
        return Ok(transaction);
    }

    // 3. 刪除帳目
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var record = _context.Transactions.Find(id);
        if (record == null) return NotFound();

        _context.Transactions.Remove(record);
        _context.SaveChanges();
        return Ok("刪除成功");
    }
}
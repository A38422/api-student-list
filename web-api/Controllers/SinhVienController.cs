using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Data;
using Microsoft.EntityFrameworkCore;
using web_api.Pagination;

namespace web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class SinhVienController : ControllerBase
    {
        private readonly MyDbContext _db;
        public SinhVienController(MyDbContext _db)
        {
            this._db = _db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVien>>> GetListStudent(string? search, int pageIndex = 1, int pageSize = 10)
        {
            var query = _db.SinhViens.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.TenSV.Contains(search) || s.MaSV.Contains(search));
            }

            var totalCount = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<SinhVien>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVien>> GetDetailStudent(int id)
        {
            var sinhVien = await _db.SinhViens.FindAsync(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            return Ok(sinhVien);
        }

        [HttpPost]
        public async Task<ActionResult<SinhVien>> CreateStudent(SinhVien sinhVien)
        {
            _db.SinhViens.Add(sinhVien);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetailStudent), new { id = sinhVien.Id }, sinhVien);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, SinhVien sinhVien)
        {
            if (id != sinhVien.Id)
            {
                return BadRequest();
            }

            _db.Entry(sinhVien).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.SinhViens.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return Ok(sinhVien);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var sinhVien = await _db.SinhViens.FindAsync(id);

            if (sinhVien == null)
            {
                return NotFound();
            }

            _db.SinhViens.Remove(sinhVien);
            await _db.SaveChangesAsync();
   
            //return NoContent();
            return Ok(sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMultipleStudents(List<int> ids)
        {
            var studentsToDelete = await _db.SinhViens.Where(s => ids.Contains(s.Id)).ToListAsync();

            if (studentsToDelete == null)
            {
                return NotFound();
            }

            _db.SinhViens.RemoveRange(studentsToDelete);
            await _db.SaveChangesAsync();

            return Ok(studentsToDelete);
        }
    }
}
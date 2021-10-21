using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Entities;
using ShopBridge.Interfaces;

namespace ShopBridge.Controllers
{
    public class InventoryController : BaseApiController
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }


        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
            try
            {
                var result = await inventoryRepository.GetItems();
                if (result.Count() == 0)
                {
                    return Ok("No item in database");
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data from database");
            }

        }

        [HttpGet("SearchById")]
        public async Task<ActionResult<Inventory>> SearchById(int Id)
        {
            try
            {
                var item = await  inventoryRepository.SearchById(Id);
                if (item == null)
                {
                    return NotFound();
                }
                return item;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data from database");
            }
        }

        [HttpGet("SearchByName")]
        public async Task<ActionResult<Inventory>> SearchByName(string name)
        {
            try
            {
                var item = await inventoryRepository.SearchByName(name);
                if (item == null)
                {
                    return NotFound();
                }
                return item;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retrieving data from database");
            }
        }


        [HttpPost("AddIItem")]
        public async Task<ActionResult> AddIItem(Inventory inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest();
                }
                var newItem = await inventoryRepository.AddItem(inventory);

                return CreatedAtAction(nameof(SearchById), new { id = newItem.Id }, newItem);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Adding new item to database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Inventory>> UpdateItem(int id, Inventory inventory)
        {
            try
            {
                if (id != inventory.Id)
                {
                    return BadRequest("Id is not matched");
                }

                var updteItem = await inventoryRepository.SearchById(id);
                if (updteItem == null)
                {
                    return NotFound($"Item with if {id} is not found.");
                }

                return await inventoryRepository.UpdateItem(inventory);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while Upating item.");
            }
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var item = await inventoryRepository.SearchById(id);
                if (item == null)
                {
                    return NotFound($"Item with if {id} is not found.");
                }
                await inventoryRepository.DeleteItem(id);

                return Ok($"Item with id {id} is deleted.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting item.");
            }
        }

    }
}

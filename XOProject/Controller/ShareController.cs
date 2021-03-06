﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XOProject.Controller
{
    [Route("api/Share")]
    public class ShareController : ControllerBase
    {
        public IShareRepository _shareRepository { get; set; }

        public ShareController(IShareRepository shareRepository)
        {
            _shareRepository = shareRepository;
        }

        [HttpPut("{symbol}")]
        public async Task<bool> UpdateLastPrice([FromRoute]string symbol)
        {
            if (symbol != null)
            {
                var share = _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).OrderByDescending(x => x.Rate).FirstOrDefault();
                if (share != null) { 
                share.Rate = +10;

                await _shareRepository.UpdateAsync(share);
                    return true;
                }
            }
            return false;

        }


        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get([FromRoute]string symbol)
        {
            var shares = _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).ToList();
            if (shares.Count >= 0)
            {
                return Ok(shares);
            }
            else
                return BadRequest();
        }


        [HttpGet("{symbol}/Latest")]
        public async Task<IActionResult> GetLatestPrice([FromRoute]string symbol)
        {
            var share =  _shareRepository.Query().Where(x => x.Symbol.Equals(symbol)).FirstOrDefault();
            if (share != null)
                return Ok(share?.Rate);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HourlyShareRate value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _shareRepository.InsertAsync(value);

            return Created($"Share/{value.Id}", value);
        }
        
    }
}

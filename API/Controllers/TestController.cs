using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class TestController : MainApiController
    {
        private readonly ITestService _testService;
        private readonly ITransactionService _transactionService;

        public TestController(ITestService testService,ITransactionService transactionService)
        {
            _testService = testService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _transactionService.FinancialTransaction();
            //await _testService.AddNewUser();
            //await _testService.AddNewRoles();
            //await _testService.InsertData();
            //await _testService.DummyData2();
            return Ok("hello world");
        }
    }
}

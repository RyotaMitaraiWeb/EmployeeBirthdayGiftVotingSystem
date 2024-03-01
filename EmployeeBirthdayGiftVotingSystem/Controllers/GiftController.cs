using EmployeeBirthdayGiftVotingSystem.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBirthdayGiftVotingSystem.Controllers
{
    [Authorize]
    public class GiftController(IGiftService giftService) : Controller
    {
        private readonly IGiftService _giftService = giftService;

        public async Task<ActionResult> All()
        {
            var gifts = await this._giftService.GetAllGifts();
            return View(gifts);
        }
    }
}

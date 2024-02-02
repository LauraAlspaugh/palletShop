namespace palletShop.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly PurchasesService _purchasesService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, PurchasesService purchasesService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _purchasesService = purchasesService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [Authorize]
  [HttpGet("purchases")]
  public async Task<ActionResult<List<Purchase>>> GetMyPurchases()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      List<Purchase> purchases = _purchasesService.GetMyPurchases(userId);
      return Ok(purchases);
    }
    catch (Exception error)
    {

      return BadRequest(error.Message);
    }
  }
  [Authorize]
  [HttpPost("purchases")]
  public async Task<ActionResult<Purchase>> ReservePurchase()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string purchase = _purchasesService.ReservePurchase(userInfo.Id);
      return Ok(purchase);
    }
    catch (Exception error)
    {

      return BadRequest(error.Message);
    }
  }
}

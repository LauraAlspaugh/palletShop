namespace palletShop.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly PurchasesService _purchasesService;
    private readonly Auth0Provider _auth0Provider;
    private readonly ListingsService _listingsService;

    public PurchasesController(PurchasesService purchasesService, Auth0Provider auth0Provider, ListingsService listingsService)
    {
        _purchasesService = purchasesService;
        _auth0Provider = auth0Provider;
        _listingsService = listingsService;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Purchase>> CreatePurchase([FromBody] Purchase purchaseData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            purchaseData.CreatorId = userInfo.Id;
            Purchase purchase = _purchasesService.CreatePurchase(purchaseData, userInfo.Id);
            return Ok(purchase);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [Authorize]
    [HttpDelete("{purchaseId}")]
    public async Task<ActionResult<string>> DestroyPurchase(int purchaseId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _purchasesService.DestroyPurchase(purchaseId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [HttpGet("{purchaseId}")]
    public async Task<ActionResult<Purchase>> GetPurchaseById(int purchaseId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Purchase purchase = _purchasesService.GetPurchaseById(purchaseId, userInfo.Id);
            return Ok(purchase);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}
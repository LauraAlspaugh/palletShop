namespace palletShop.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReceiptsController : ControllerBase
{
    private readonly ReceiptsService _receiptsService;
    private readonly Auth0Provider _auth0Provider;

    public ReceiptsController(ReceiptsService receiptsService, Auth0Provider auth0Provider)
    {
        _receiptsService = receiptsService;
        _auth0Provider = auth0Provider;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Receipt>> CreateReceipt([FromBody] Receipt receiptData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            receiptData.CreatorId = userInfo.Id;
            Receipt receipt = _receiptsService.CreateReceipt(receiptData, userInfo.Id);
            return Ok(receipt);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}
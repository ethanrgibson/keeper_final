using System.Threading.Tasks;

namespace keeper_final.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{

  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultKeepsService _vaultKeepsService;
  private readonly KeepsService _keepsService;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, KeepsService keepsService, VaultKeepsService vaultKeepsService)
  {
    _vaultsService = vaultsService;
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
    _vaultKeepsService = vaultKeepsService;
  }



  [Authorize]
  [HttpPost]

  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault vault = _vaultsService.CreateVault(vaultData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{vaultId}")]

  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)

  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo);
      return Ok(vault);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  [Authorize]
  [HttpPut("{vaultId}")]

  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.UpdateVault(vaultId, userInfo, vaultUpdateData);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultId}")]

  public async Task<ActionResult<string>> DeleteVaultAsync(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultsService.DeleteVault(vaultId, userInfo);
      return Ok(message);

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]

  public async Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<VaultKeepKept> keeps = _vaultKeepsService.GetKeepsByVaultId(vaultId, userInfo);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }



}
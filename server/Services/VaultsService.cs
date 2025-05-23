


namespace keeper_final.Services;

public class VaultsService
{

  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.CreateVault(vaultData);
    return vault;
  }


  private Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetVaultById(vaultId);

    if (vault == null)
    {
      throw new Exception("Vault Does Not Exist");
    }
    return vault;
  }


  internal Vault GetVaultById(int vaultId, Account userInfo)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.IsPrivate == true && vault.CreatorId != userInfo?.Id)
    {
      throw new Exception("Cannot Access A Private Vault");
    }

    return vault;
  }

  internal Vault UpdateVault(int vaultId, Account userInfo, Vault vaultUpdateData)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT UPDATE A VAULT THAT IS NOT YOURS");
    }

    vault.Name = vaultUpdateData.Name ?? vault.Name;
    vault.IsPrivate = vaultUpdateData.IsPrivate ?? vault.IsPrivate;

    _repository.UpdateVault(vault);

    return vault;
  }

  internal string DeleteVault(int vaultId, Account userInfo)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("YOU CANNOT DELETE A VAULT THAT IS NOT YOURS!");
    }
    _repository.DeleteVault(vaultId);

    return vault.Name + " has been deleted!";

  }

  internal List<Vault> GetVaultsByAccountId(string accountId)
  {
    List<Vault> vaults = _repository.GetVaultsByAccountId(accountId);
    return vaults;
  }


  internal List<Vault> GetVaultsByAccountId(string accountId, Account userInfo)
  {
    List<Vault> vaults = GetVaultsByAccountId(accountId);

    return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userInfo?.Id);
  }



}
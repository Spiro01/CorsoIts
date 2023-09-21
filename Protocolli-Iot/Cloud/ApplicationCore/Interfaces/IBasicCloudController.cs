using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces;

public interface IBasicCloudController <TEntity,TKey>
{
    
    public Task<IActionResult> Get(TKey id);
    public Task<IActionResult> Insert(TEntity entity);
    public Task<IActionResult> Update(TEntity entity);
    public Task<IActionResult> Delete(TEntity entity);
}
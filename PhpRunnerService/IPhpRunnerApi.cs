using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhpRunnerService
{
    //[Headers("x-api-key: b95bfb30-55bc-4327-bb8b-35d740f70051")]
    //[Headers("Content-Type: application/json")]
    // [Headers("Content-Type: application/x-www-form-urlencoded")]
    //[Headers("Content-Type: multipart/form-data")]
    public interface IPhpRunnerApi<T>
    {
        [Get("/v1.php?table={table}&action=view&editid1={id}")]
        Task<string> GetItem(string table, string id);

        [Get("/v1.php?table={table}&action=list")]
        Task<string> ListItems(string table);

        [Get("/v1.php?table={table}&action=list&q=({field}~{filter.Value}~{value})")]
        Task<string> SearchItems(string table, string field, Enum.PhpRunnerFilter filter, string value);

        [Post("/v1.php?table={table}&action=update&editid1={id}")]
        Task<string> UpdateItem(string table, string id, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> obj);

        [Post("/v1.php?table={table}&action=insert")]
        Task<string> InsertItem(string table, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> obj);

        [Get("/v1.php?table={table}&action=delete&editid1={id}")]
        Task<string> DeleteItem(string table, string id);
    }
}

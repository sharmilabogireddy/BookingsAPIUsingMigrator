using Microsoft.AspNetCore.JsonPatch;

namespace BookingsAPIUsingMigrator.api.Models.RequestModel
{
    /// <summary>
    /// Request Model used for patching objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PatchRequestModel<T>
        where T : class
    {
        /// <summary>
        /// Json Patch Document with the patching data
        /// </summary>
        public required JsonPatchDocument<T> PatchData { get; set; }
    }
}

using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Accounts.Data;
using HotChocolate;
using HotChocolate.ApolloFederation;
using HotChocolate.Language;

namespace Accounts.Models
{
    [ReferenceResolver(EntityResolverType = typeof(UserReferenceResolver))]
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        // [ReferenceResolver]
        // public async Task<User> GetAsync(string id)
        // {
        //     // some code ....
        //     return new User();
        // }
    }

    public static class UserReferenceResolver
    {
        public static async Task<User> GetUserReferenceResolverAsync(
            [LocalState] ObjectValueNode data,
            [Service] UserRepository userRepository)
        {
            // some code ....
            return userRepository.GetUserById((string)data.Fields.First(field => field.Name.Value.Equals("id")).Value.Value);
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NuGet.Services.Entities;
using Microsoft.Web.Helpers;
using System.Net.Http;
using NuGetGallery.Helpers;

namespace NuGetGallery
{
    public class GravatarProxyService : IGravatarProxyService
    {
        private const string PngExtension = "png";

        private readonly HttpClient _httpClient;
        private readonly IEntityRepository<User> _users;
        private readonly ILogger<GravatarProxyService> _logger;

        public GravatarProxyService(
            HttpClient httpClient,
            IEntityRepository<User> users,
            ILogger<GravatarProxyService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Stream> GetProfilePictureOrNull(string username, int imageSize)
        {
            var user = _users.GetAll().FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                _logger.LogWarning("Could not find an account with username {Username}", username);
                return null;
            }

            try
            {
                var url = GravatarHelper.Url(user.EmailAddress ?? user.UnconfirmedEmailAddress, imageSize);

                return await _httpClient.GetStreamAsync(url);
            }
            catch (Exception e)
            {
                _logger.LogError(0, e, "Unable to fetch profile picture for user {Username}", username);
                return null;
            }
        }
    }
}

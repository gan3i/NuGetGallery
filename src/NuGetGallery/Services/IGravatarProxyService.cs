// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Threading.Tasks;

namespace NuGetGallery
{
    /// <summary>
    /// Fetches Gravatar profile pictures.
    /// </summary>
    public interface IGravatarProxyService
    {
        /// <summary>
        /// Fetch an account's profile picture.
        /// </summary>
        /// <param name="username">The account's username.</param>
        /// <param name="imageSize">The desired image size in pixels.</param>
        /// <returns>
        /// The profile's picture in PNG file format, or <see langword="null"/> if the
        /// username does not exist.
        /// </returns>
        Task<Stream> GetProfilePictureOrNull(string username, int imageSize);
    }
}

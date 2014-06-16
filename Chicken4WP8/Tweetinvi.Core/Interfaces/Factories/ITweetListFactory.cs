﻿using System.Collections.Generic;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Interfaces.Models;

namespace Tweetinvi.Core.Interfaces.Factories
{
    public interface ITweetListFactory
    {
        ITweetList CreateList(string name, PrivacyMode privacyMode, string description);

        ITweetList GetExistingTweetList(ITweetList list);
        ITweetList GetExistingTweetList(ITweetListDTO listDTO);
        ITweetList GetExistingTweetList(long listId);
        ITweetList GetExistingTweetList(string slug, IUser user);
        ITweetList GetExistingTweetList(string slug, IUserIdentifier userDTO);
        ITweetList GetExistingTweetList(string slug, long userId);
        ITweetList GetExistingTweetList(string slug, string userScreenName);

        ITweetList GenerateTweetListFromDTO(ITweetListDTO tweetListDTO);
        IEnumerable<ITweetList> GenerateTweetListsFromDTO(IEnumerable<ITweetListDTO> tweetListsDTO);
        
        ITweetList GenerateTweetListFromJson(string jsonList);
    }
}
﻿using System.Collections.Generic;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.Parameters;

namespace Tweetinvi.Core.Interfaces.Controllers
{
    public interface ITweetListController
    {
        IEnumerable<ITweetList> GetUserLists(IUser user, bool getOwnedListsFirst);
        IEnumerable<ITweetList> GetUserLists(IUserIdentifier userDTO, bool getOwnedListsFirst);
        IEnumerable<ITweetList> GetUserLists(long userId, bool getOwnedListsFirst);
        IEnumerable<ITweetList> GetUserLists(string userScreenName, bool getOwnedListsFirst);

        ITweetList UpdateList(ITweetList tweetList, IListUpdateParameters parameters);
        ITweetList UpdateList(ITweetListDTO tweetListDTO, IListUpdateParameters parameters);
        ITweetList UpdateList(long listId, IListUpdateParameters parameters);
        ITweetList UpdateList(string slug, IUser owner, IListUpdateParameters parameters);
        ITweetList UpdateList(string slug, IUserIdentifier ownerDTO, IListUpdateParameters parameters);
        ITweetList UpdateList(string slug, long ownerId, IListUpdateParameters parameters);
        ITweetList UpdateList(string slug, string ownerScreenName, IListUpdateParameters parameters);
        ITweetList UpdateList(IListIdentifier identifier, IListUpdateParameters parameters);

        bool DestroyList(ITweetList tweetList);
        bool DestroyList(ITweetListDTO tweetListDTO);
        bool DestroyList(long listId);
        bool DestroyList(string slug, IUser owner);
        bool DestroyList(string slug, IUserIdentifier ownerDTO);
        bool DestroyList(string slug, string ownerScreenName);
        bool DestroyList(string slug, long ownerId);
        bool DestroyList(IListIdentifier identifier);

        IEnumerable<ITweet> GetTweetsFromList(ITweetList tweetList);
        IEnumerable<ITweet> GetTweetsFromList(ITweetListDTO tweetListDTO);
        IEnumerable<ITweet> GetTweetsFromList(long listId);
        IEnumerable<ITweet> GetTweetsFromList(string slug, IUser owner);
        IEnumerable<ITweet> GetTweetsFromList(string slug, IUserIdentifier ownerDTO);
        IEnumerable<ITweet> GetTweetsFromList(string slug, string ownerScreenName);
        IEnumerable<ITweet> GetTweetsFromList(string slug, long ownerId);
        IEnumerable<ITweet> GetTweetsFromList(IListIdentifier identifier);

        IEnumerable<IUser> GetMembersOfList(ITweetList tweetList, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(ITweetListDTO tweetListDTO, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(long listId, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(string slug, IUser owner, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(string slug, IUserIdentifier ownerDTO, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(string slug, string ownerScreenName, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(string slug, long ownerId, int maxNumberOfUsersToRetrieve = 100);
        IEnumerable<IUser> GetMembersOfList(IListIdentifier identifier, int maxNumberOfUsersToRetrieve = 100);
    }
}
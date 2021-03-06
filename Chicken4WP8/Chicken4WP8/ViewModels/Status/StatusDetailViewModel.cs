﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Chicken4WP8.Common;
using Chicken4WP8.Controllers;
using Chicken4WP8.Controllers.Interface;
using Chicken4WP8.Models.Setting;
using Chicken4WP8.Services.Interface;
using Chicken4WP8.ViewModels.Base;

namespace Chicken4WP8.ViewModels.Status
{
    public class StatusDetailViewModel : PivotItemViewModelBase<ITweetModel>
    {
        #region properties
        protected ITweetModel status;
        protected ITweetController statusController;
        protected IUserController userController;

        public StatusDetailViewModel(
            IEventAggregator eventAggregator,
            ILanguageHelper languageHelper,
            IEnumerable<Lazy<ITweetController, OAuthTypeMetadata>> statusControllers,
            IEnumerable<Lazy<IUserController, OAuthTypeMetadata>> userControllers)
            : base(eventAggregator, languageHelper)
        {
            statusController = statusControllers.Single(c => c.Metadata.OAuthType == App.UserSetting.OAuthSetting.OAuthSettingType).Value;
            userController = userControllers.Single(c => c.Metadata.OAuthType == App.UserSetting.OAuthSetting.OAuthSettingType).Value;
        }
        #endregion

        protected async override void OnInitialize()
        {
            base.OnInitialize();
            if (Items == null)
                Items = new ObservableCollection<ITweetModel>();

            await ShowProgressBar();
            //initialize the tweet from cache
            status = StorageService.GetTempTweet();
            status.IsStatusDetail = true;
            if (status.RetweetedStatus != null)
                status.RetweetedStatus.IsStatusDetail = true;
            Items.Add(status);
            await HideProgressBar();
        }

        protected override void SetLanguage()
        {
            DisplayName = LanguageHelper["StatusDetailViewModel_Header"];
        }

        protected override void ItemClicked(object item)
        {
            var tweet = item as ITweetModel;
            if (tweet.Id == status.Id)
                return;
            base.ItemClicked(item);
        }

        protected override Task RealizeItem(ITweetModel item)
        {
            var user = item.RetweetedStatus == null ? item.User : item.RetweetedStatus.User;
            if (user.ProfileImageData == null)
                Task.Factory.StartNew(() => userController.SetProfileImageAsync(user));
            if (item.IsStatusDetail)
                Task.Factory.StartNew(() => statusController.SetTweetImagesAsync(item));
            return Task.Delay(0);
        }

        protected async override Task FetchDataFromWeb()
        {
            var id = Items[0].InReplyToTweetId;
            if (id == null)
                return;
            await ShowProgressBar();
            var option = Const.GetDictionary();
            option.Add(Const.ID, id);
            var tweet = await statusController.ShowAsync(option);
            Items.Insert(0, tweet);
            listbox.ScrollTo(Items[0]);
            await HideProgressBar();
        }

        protected override Task LoadDataFromWeb()
        {
            return Task.Delay(0);
        }

        public void AppBar_Reply()
        {
            var newTweet = new NewTweetModel();
            newTweet.InReplyToStatusId = status.Id;
            var names = new List<string>();
            names.Add("@" + status.User.ScreenName);
            if (status.RetweetedStatus != null && status.RetweetedStatus.User != null)
                names.Add("@" + status.RetweetedStatus.User.ScreenName);
            if (status.Entities.UserMentions != null && status.Entities.UserMentions.Count != 0)
            {
                var users = status.Entities.UserMentions
                    .Where(m => m.ScreenName != App.UserSetting.ScreenName)
                    .Select(m => "@" + m.ScreenName);
                foreach (var user in users)
                {
                    if (!names.Contains(user))
                        names.Add(user);
                }
            }

            string text = string.Join(" ", names) + " ";
            newTweet.InReplyToUserName = "@" + status.User.ScreenName;
            newTweet.Text = text;
            newTweet.Type = NewTweetType.Reply;
            StorageService.UpdateTempNewTweet(newTweet);
            NavigationService.UriFor<NewStatusPageViewModel>()
                 .WithParam(o => o.Random, DateTime.Now.Ticks.ToString("x"))
                 .Navigate();
        }
    }
}

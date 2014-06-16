﻿using System.Collections.Generic;
using Tweetinvi.Core.Interfaces.DTO;

namespace Tweetinvi.Core.Interfaces.Factories
{
    public interface IMessageFactory
    {
        // Get existing message
        IMessage GetExistingMessage(long messageId);

        // Create message
        IMessage CreateMessage(string text, IUser recipient = null);

        // Generate message from DTO
        IMessage GenerateMessageFromMessageDTO(IMessageDTO messageDTO);
        IEnumerable<IMessage> GenerateMessagesFromMessagesDTO(IEnumerable<IMessageDTO> messagesDTO);

        // Generate Message from Json
        IMessage GenerateMessageFromJson(string jsonMessage);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Commands;

namespace Social.Command.Api.Commands
{
    public class RemoveCommentCommand : BaseCommand
    {
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}
using ChatApp.Controllers;
using ChatApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    public class CustomUserIdProvider :  IUserIdProvider
    {

        private readonly IHttpContextAccessor _httpContextAccesor;

        public CustomUserIdProvider(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }
        public virtual string GetUserId(HubConnectionContext connection)
        {
          
          
           var  id = "";
            return id;
        }
    }
}

﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using papuff.domain.Core.Users;
using papuff.domain.Helpers;
using papuff.domain.Interfaces.Services.Core;
using papuff.services.Validators.Notifications.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace papuff.webapi.Controllers.Base {
    public class BaseController : ControllerBase {
        
        #region - parameters -

        private IServiceUser _user =>
            (IServiceUser)HttpContext.RequestServices
                .GetService(typeof(IServiceUser));

        protected IEventNotifier _notify =>
            (IEventNotifier)HttpContext.RequestServices
                .GetService(typeof(IEventNotifier));

        #endregion

        #region - ctor -

        /// <summary>
        /// ctor
        /// </summary>
        protected BaseController() {

        }

        #endregion

        #region - call -

        /// <summary>
        /// return new ObjectResponse or message notification
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult Result(object result = null) {
            try {
                if (!_notify.IsValid)
                    return BadRequest(_notify.GetNotifications());

                if (result == null)
                    return Ok();

                return Ok(result);
            } catch (ArgumentException ex) {
                return NotFound(ex);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        #endregion

        #region - session -

        /// <summary>
        /// return user info from current context
        /// </summary>
        /// <returns></returns>
        protected User Logged => Task.Run(async () => await _user.GetMe(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? ""))?.Result;

        /// <summary>
        /// return user info from current context
        /// </summary>
        /// <returns></returns>
        protected string LoggedLess => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        #endregion

        #region - methods -

        /// <summary>
        /// return server info used for request
        /// </summary>
        /// <returns></returns>
        protected string ServerUri() => Request.GetDisplayUrl();

        /// <summary>
        /// return ip from request context
        /// </summary>
        /// <returns></returns>
        protected string RequestIp() => HttpContext.Connection.RemoteIpAddress.ToString();

        #endregion

        #region - components -

        protected List<SelectListItem> ToDropDown<T>(IEnumerable<T> list, string text, string value) {
            var dropdown = list.Select(item => new SelectListItem {
                Text = item.GetType()
                    .GetProperty(text)
                    .GetValue(item, null) as string, 
                
                Value = item.GetType()
                    .GetProperty(value)
                    .GetValue(item, null) as string
            }).ToList();
            
            return dropdown.OrderBy(x => x.Text).ToList();
        }

        /// <summary>
        /// Create a Dropdown Enum
        /// </summary>
        /// <typeparam name="T">Generic Enum</typeparam>
        /// <param name="prop">Exclude attribute</param>
        /// <returns></returns>
        protected List<SelectListItem> ToEnumDropDown<T>(string prop = "") {
            var dropdown = new List<SelectListItem>();
            foreach (var i in Enum.GetValues(typeof(T))) {
                if (prop == Enum.GetName(typeof(T), i)) continue;
                var sItem = new SelectListItem {
                    Text = (i as Enum).EnumDisplay(), Value = ((int) i).ToString()
                };
                dropdown.Add(sItem);
            }
            return dropdown.OrderBy(x => x.Text).ToList();
        }

        #endregion
    }
}
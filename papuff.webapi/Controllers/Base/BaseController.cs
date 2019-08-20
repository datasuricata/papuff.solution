using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using papuff.domain.Core.Users;
using papuff.domain.Helpers;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Notifications.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace papuff.webapi.Controllers.Base {
    //[Authorize]
    public class BaseController : ControllerBase {
        #region [ parameters ]

        private IServiceUser serviceUser =>
            (IServiceUser)HttpContext.RequestServices.GetService(typeof(IServiceUser));

        private IEventNotifier notifier =>
            (IEventNotifier)HttpContext.RequestServices.GetService(typeof(IEventNotifier));

        #endregion

        #region [ ctor ]

        /// <summary>
        /// ctor
        /// </summary>
        protected BaseController() {

        }

        #endregion

        /// <summary>
        /// return new ObjectResponse or message notification
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult Result(object result = null) {
            try {
                if (notifier.HasAny())
                    return BadRequest(notifier.GetNotifications());

                if (result == null)
                    return Ok();

                return Ok(result);
            } catch (ArgumentException ex) {
                return NotFound(ex);
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }


        #region [ user session ]

        /// <summary>
        /// return user info from current context
        /// </summary>
        /// <returns></returns>
        protected User Logged {
            get {
                return serviceUser?.GetMe(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            }
        }

        /// <summary>
        /// return user info from current context
        /// </summary>
        /// <returns></returns>
        protected string LoggedLess {
            get {
                return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }

        #endregion

        #region [ methods ]

        /// <summary>
        /// return server info used for request
        /// </summary>
        /// <returns></returns>
        protected string ServerUri() {
            return Request.GetDisplayUrl();
        }

        /// <summary>
        /// return ip from request context
        /// </summary>
        /// <returns></returns>
        protected string RequestIp() {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        #endregion

        #region [ components ]

        protected List<SelectListItem> ToDropDown<T>(List<T> list, string text, string value) {
            List<SelectListItem> dropdown = new List<SelectListItem>();
            foreach (var item in list) {
                var sItem = new SelectListItem();
                sItem.Text = item.GetType().GetProperty(text).GetValue(item, null) as string;
                sItem.Value = item.GetType().GetProperty(value).GetValue(item, null) as string;
                dropdown.Add(sItem);
            }

            return dropdown.OrderBy(x => x.Text).ToList();
        }

        /// <summary>
        /// Create a Dropdown Enum
        /// </summary>
        /// <typeparam name="T">Generic Enum</typeparam>
        /// <param name="excludeProps">Exclude attribute</param>
        /// <returns></returns>
        protected List<SelectListItem> ToEnumDropDown<T>(string excludeProps = "") {
            List<SelectListItem> dropdown = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(typeof(T))) {
                if (excludeProps != Enum.GetName(typeof(T), item)) {
                    var sItem = new SelectListItem();
                    sItem.Text = Helpers.EnumDisplay(item as Enum);
                    sItem.Value = ((int)item).ToString();
                    dropdown.Add(sItem);
                }
            }
            return dropdown.OrderBy(x => x.Text).ToList();
        }

        #endregion
    }
}

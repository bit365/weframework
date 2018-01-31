using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Web.Core.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using WeFramework.Service.Navigates;
using WeFramework.Web.Models.Navigates;
using WeFramework.Core.Utility;
using WeFramework.Core.Domain.Navigates;
using WeFramework.Web.Core.Security;

namespace WeFramework.Web.Controllers
{
    [ActionAuthorize]
    public class NavigateController : BaseController
    {
        private readonly IMapper mapper;

        private readonly IConfigurationProvider mapperConfigProvider;

        private readonly INavigateService navigateService;

        public NavigateController(INavigateService navigateService, IMapper mapper, IConfigurationProvider mapperConfigProvider)
        {
            this.navigateService = navigateService;
            this.mapperConfigProvider = mapperConfigProvider;
            this.mapper = mapperConfigProvider.CreateMapper();
        }

        public ActionResult Index()
        {
            var nvas = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(mapperConfigProvider);
            return View(nvas.OrderBy(nav => nav.SortOrder));
        }

        [HttpPost]
        public ActionResult Delete(int[] check)
        {
            check = check ?? new int[0];
            Array.ForEach(check, id => navigateService.Delete(id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ActionAuthorize("NavigateEdit")]
        public ActionResult Save(FormCollection form)
        {
            string navSortOrderPrefix = "nav-";

            foreach (string key in form.Keys)
            {
                if (key.StartsWith(navSortOrderPrefix) && !string.IsNullOrEmpty(form[key]) && form[key].All(c => Char.IsNumber(c)))
                {
                    var nav = navigateService.GetNavigate(Convert.ToInt32(key.TrimStart(navSortOrderPrefix)));
                    nav.SortOrder = Convert.ToInt32(form[key]);
                    navigateService.Update(nav);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create(int? parentID = null)
        {
            ViewBag.Navigates = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(mapperConfigProvider);
            ViewBag.ParentID = parentID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NavigateModel model)
        {
            if (ModelState.IsValid)
            {
                Navigate navigate = mapper.Map<NavigateModel, Navigate>(model);
                navigateService.CreateNavigate(navigate);
                return RedirectToAction("Index");
            }
            ViewBag.Navigates = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(mapperConfigProvider);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Navigates = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(mapperConfigProvider);
            return View(mapper.Map<NavigateModel>(navigateService.GetNavigate(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NavigateModel model)
        {
            if (ModelState.IsValid)
            {
                Navigate navigate = mapper.Map(model,navigateService.GetNavigate(model.ID));
                navigateService.Update(navigate);
                return RedirectToAction("Index");
            }
            ViewBag.Navigates = navigateService.GetNavigates().AsQueryable().ProjectTo<NavigateModel>(mapperConfigProvider);

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Icons()
        {
            return PartialView("Icons");
        }
    }
}
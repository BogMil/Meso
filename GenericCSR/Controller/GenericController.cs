using System;
using System.Linq;
using System.Web.Mvc;
using GenericCSR.Service;
using PagedList;

namespace GenericCSR.Controller
{

    public class GenericController<TService, TViewModel, TQueryDto, TCommandDto> 
        : System.Web.Mvc.Controller 
            
        where TViewModel : GenericJqGridViewModel<TQueryDto>, new()
        where TQueryDto : class
        where TCommandDto : class
        where TService : IGenericService<TQueryDto,TCommandDto>
    {
        protected TService Service;

        public GenericController(TService service)
        {
            Service = service;
        }

        public virtual ActionResult AjaxJqGrid(Pager pager,OrderByProperties orderByProperties,string filters)
        {
            var data = Service.GetJqGridData(pager, filters,orderByProperties);
            var jqGridViewModal = JqGridViewModelFactory(data);
            return Json(jqGridViewModal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult AjaxCreate(TCommandDto dto)
        {
            try
            {
                Service.Create(dto);
                return Json(SuccessMessageCreator.GetMessage(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ErrorMessageCreator.GetMessage(e), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual ActionResult AjaxUpdate(TCommandDto dto)
        {
            try
            {
                Service.Update(dto);
                return Json(SuccessMessageCreator.GetMessage(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ErrorMessageCreator.GetMessage(e), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual ActionResult AjaxDelete(int id)
        {
            try
            {
                Service.Delete(id);
                return Json(SuccessMessageCreator.GetMessage(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(ErrorMessageCreator.GetMessage(e), JsonRequestBehavior.AllowGet);
            }
        }

        private static TViewModel JqGridViewModelFactory(StaticPagedList<TQueryDto> listOfDto)
        {
            var viewModel = new TViewModel
            {
                CurrentPageNumber = listOfDto.PageNumber,
                TotalNumberOfPages = listOfDto.PageCount,
                TotalNumberOfRecords = listOfDto.TotalItemCount,
                Records = listOfDto.ToList()
            };

            return viewModel;
        }

        public static TReturnViewModel JqGridViewModelFactory<TReturnViewModel, QueryDto>(StaticPagedList<QueryDto> listOfDto) 
            where TReturnViewModel:GenericJqGridViewModel<QueryDto>,new()
            where QueryDto :class
        {
            var viewModel = new TReturnViewModel
            {
                Records = listOfDto.ToList(),
                CurrentPageNumber = listOfDto.PageNumber,
                TotalNumberOfPages = listOfDto.PageCount,
                TotalNumberOfRecords = listOfDto.TotalItemCount,
            };

            return viewModel;
        }
    }
}
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XpoDemoEngland.Module.BusinessObjects;

namespace XpoDemoEngland.Module.Controllers
{
    public class CriteriaOperatorsController : ViewController
    {
        SimpleAction QueryWithCriteria;
        public CriteriaOperatorsController() : base()
        {
            QueryWithCriteria = new SimpleAction(this, "QueryWithCriteria", "View");
            QueryWithCriteria.Execute += QueryWithCriteria_Execute;
            // Target required Views (use the TargetXXX properties) and create their Actions.

        }
        protected override void OnActivated()
        {
            base.OnActivated();
           
            
            // Perform various tasks depending on the target View.
        }
        private void QueryWithCriteria_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var IsActive=new BinaryOperator("Active", true, BinaryOperatorType.Equal);
            var Code = new BinaryOperator("Code", "001", BinaryOperatorType.Equal);
            var FinalOperator=  CriteriaOperator.And(IsActive, Code);
            var FinalOperatorText = FinalOperator.ToString();
            var FinalOperatorObject = CriteriaOperator.Parse(FinalOperatorText);

            var Reult2 =this.View.ObjectSpace.FindObject<Entity>(FinalOperatorObject);
            var Result1=this.View.ObjectSpace.CreateCollection(typeof(Entity),FinalOperatorObject);
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
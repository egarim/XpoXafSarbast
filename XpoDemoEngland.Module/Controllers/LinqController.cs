using DevExpress.Data.Filtering;
using DevExpress.DataAccess.Sql;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XpoDemoEngland.Module.BusinessObjects;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace XpoDemoEngland.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class LinqController : ViewController
    {
        SimpleAction SqlQuery;
        SimpleAction ExecuteLinqQuery;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public LinqController()
        {
            InitializeComponent();
            ExecuteLinqQuery = new SimpleAction(this, "Linq Query", "View");
            ExecuteLinqQuery.Execute += ExecuteLinqQuery_Execute;

            SqlQuery = new SimpleAction(this, "Sql Query", "View");
            SqlQuery.Execute += SqlQuery_Execute;
            


            this.TargetObjectType = typeof(LinqQueryResults);

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void SqlQuery_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var QueryOs = this.Application.CreateObjectSpace(typeof(Entity));
            var XpObjectSpace = ((XPObjectSpace)QueryOs);
            UnitOfWork UoW = new UnitOfWork(XpObjectSpace.Session.DataLayer);

            var SelectedData=UoW.ExecuteQuery("Select Oid,Name from Entity");
            var Params = this.View.CurrentObject as LinqQueryResults;

            foreach (DevExpress.Xpo.DB.SelectStatementResultRow selectStatementResultRow in SelectedData.ResultSet[0].Rows)
            {
                  Params.Results.Add(new LinqResult() { Name = selectStatementResultRow.Values[1].ToString(), EntityOid = selectStatementResultRow.Values[0].ToString() });
            }
            //foreach (var item in Result)
            //{
            //    Params.Results.Add(new LinqResult() { Name = item.Name, EntityOid = item.Oid.ToString() });

            //}
        }
        private void ExecuteLinqQuery_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var Params = this.View.CurrentObject as LinqQueryResults;

            var QueryOs = this.Application.CreateObjectSpace(typeof(Entity));

            var MyQuery= QueryOs.GetObjectsQuery<Entity>();

            var Result = (from Entity in MyQuery where(Entity.Name== Params.Name && Entity.Active) select new { Entity.Name, Entity.Oid });
            
            
            foreach (var item in Result)
            {
                Params.Results.Add(new LinqResult() { Name = item.Name ,EntityOid= item.Oid.ToString()});

            }
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}

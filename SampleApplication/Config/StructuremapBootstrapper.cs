using System.Web.Http;
using System.Web.Mvc;
using CC.UI.Helpers.CoreConfiguration;
using CC.UI.Helpers.InstanceConfiguration.HtmlConventionRegistries;
using CC.UI.Helpers.TagConfiguration;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace SampleApplication.Config
{
    public class StructureMapBootstrapper
    {
        public static void Bootstrap()
        {
            new StructureMapBootstrapper().Start();
        }
        private void Start()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(new WebRegistry());
            });
            //            ObjectFactory.AssertConfigurationIsValid();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(ObjectFactory.Container);


        }
    }

    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType<TagProfileExpression>();
                    x.WithDefaultConventions();
                });

            For<TagProfileExpression>().Singleton().Use<CCHtmlConventions>();
            For<IElementNamingConvention>().Use<CCElementNamingConvention>();
            For(typeof (ITagGenerator<>)).Use(typeof (TagGenerator<>));
        }
    }
}
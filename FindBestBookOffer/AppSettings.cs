using System.Configuration;

namespace FindBestBookOffer
{
    public class AppSettings
    {
        private readonly string accessKey;
        private readonly string amazonWebServiceNameSpace;
        private readonly string associateTag;
        private readonly string secretKey;

        public AppSettings()
        {
            SetPropertyFromConfigurationManager(ref amazonWebServiceNameSpace, "AmazonWebServiceNameSpace");
            SetPropertyFromConfigurationManager(ref accessKey, "AccessKey");
            SetPropertyFromConfigurationManager(ref secretKey, "SecretKey");
            SetPropertyFromConfigurationManager(ref associateTag, "AssociateTag");
        }

        public string AccessKey
        {
            get { return accessKey; }
        }

        public string SecretKey
        {
            get { return secretKey; }
        }

        public string AssociateTag
        {
            get { return associateTag; }
        }

        public string AmazonWebServiceNameSpace
        {
            get { return amazonWebServiceNameSpace; }
        }

        private void SetPropertyFromConfigurationManager(ref string property, string configurationManagerKey)
        {
            var valueFromConfigurationManager = ConfigurationManager.AppSettings[configurationManagerKey];
            if (!string.IsNullOrEmpty(valueFromConfigurationManager) &&
                valueFromConfigurationManager != "PLEASE_UPDATE_THIS_VALUE")
            {
                property = valueFromConfigurationManager;
            }
            else
            {
                var exceptionMessage =
                    string.Format(
                        "There is no key named: {0} in app.config or this value is not properly configured. Please check your app.config file.",
                        configurationManagerKey);
                throw new ConfigurationErrorsException(exceptionMessage);
            }
        }
    }
}
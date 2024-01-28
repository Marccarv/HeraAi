using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeraAI.API.Enums;
using HeraAI.API.Models.Specs.Others.Properties;
using HeraAI.API.Tools.DataInterchange;


namespace HeraAI.API.Models.Specs
{

    public class BaseEntitySpecs
    {

        // READONLY VARIABLES
        private static readonly string currentNamespace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private static readonly string currentClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;


        // STATE VARIABLES
        private static BoolProperty inactive;
        private static DateTimeProperty creationDate;
        private static DateTimeProperty lastUpdate;
        private static BoolProperty updating;


        /// <summary>
        /// Inactive property
        /// </summary>
        public static BoolProperty Inactive
        {

            get
            {

                if (inactive is null)
                {
                    inactive = new BoolProperty("Inactive", false);
                }

                return inactive;

            }

        }


        /// <summary>
        /// Creation Date property
        /// </summary>
        public static DateTimeProperty CreationDate
        {

            get
            {

                if (creationDate is null)
                {
                    creationDate = new DateTimeProperty("CreationDate", false, DateTimeAllowedPeriods.pastToPresent, DateTimeFormats.dateTime);
                }

                return creationDate;

            }

        }


        /// <summary>
        /// Last Update property
        /// </summary>
        public static DateTimeProperty LastUpdate
        {

            get
            {

                if (lastUpdate is null)
                {

                    lastUpdate = new DateTimeProperty("LastUpdate", true, DateTimeAllowedPeriods.undefinedPast, DateTimeFormats.fullDateTime);

                }

                return lastUpdate;

            }

        }


        /// <summary>
        /// Updating property
        /// </summary>
        public static BoolProperty Updating
        {

            get
            {

                if (updating is null)
                {
                    updating = new BoolProperty("Updating", false);
                }

                return updating;

            }

        }


        /// <summary>
        /// Serializes a list of objects to json format
        /// </summary>
        /// <param name="obj">Object to be serialized</param>
        /// <returns>String with a json representation of the list of object</returns>
        public static string GetJson(object obj)
        {
            return Json.Serialize(obj, GlobalVars.GetDateTimeDataInterchangeRepresentation());
        }

    }

}

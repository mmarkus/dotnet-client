﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infinispan.DotNetClient.Operations;
using Infinispan.DotNetClient;
using Infinispan.DotNetClient.Protocol;
using Infinispan.DotNetClient.Trans;
using NLog;

namespace Infinispan.DotNetClient.Operations
{
    /*
    * Retrieves detils of the remote cache
    *  
    * Author: sunimalr@gmail.com
    * 
    */
    public class StatsOperation : HotRodOperation
    {
        private static Logger logger;
        public StatsOperation(Codec codec, byte[] cacheName, int topologyId, Flag[] flags) :
            base(codec, flags, cacheName, topologyId)
        {
            logger = LogManager.GetLogger("StatsOoperation");
        }

        public Dictionary<string, string> executeOperation(Transport transport)
        {
            //Defines a new Dictonary which is capable of storing indexed string values with a string index.
            Dictionary<string, string> result;
            //Writes the request header
            HeaderParams param = writeHeader(transport, STATS_REQUEST);
            transport.flush();
            readHeaderAndValidate(transport, param);
            //reads the number of statistic details sent from server
            int numberOfStats = transport.readVInt();
            if (logger.IsTraceEnabled)
                logger.Trace("Number of Stats : " + numberOfStats);
            result = new Dictionary<string, string>();
            //reads all statistics and add them to the 'result' dictionary
            for (int i = 0; i < numberOfStats; i++)
            {
                String statName = transport.readString();
                if (logger.IsTraceEnabled)
                    logger.Trace("Stat Name Recieved : " + statName);
                String statValue = transport.readString();
                if (logger.IsTraceEnabled)
                    logger.Trace("Stat ValueRecieved : " + statName);
                result.Add(statName, statValue);
            }
            return result;
        }
    }
}

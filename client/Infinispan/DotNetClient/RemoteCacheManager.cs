﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infinispan.DotNetClient.Util;
using Infinispan.DotNetClient.Protocol;
using Infinispan.DotNetClient.Trans.TCP;
using Infinispan.DotNetClient.Operations;
using Infinispan.DotNetClient.Trans;


namespace Infinispan.DotNetClient
{

    /// <summary>
    /// Aggregates RemoteCaches and lets user to get hold of a remotecache.
    /// Author: sunimalr@gmail.com
    /// </summary>
    public class RemoteCacheManager
    {
        private ClientConfig config;
        private Serializer serializer;
        private Codec codec;
        private TCPTransportFactory transportFactory;

        
        /// <summary>
        /// Constructor with specified serializer s
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="s"></param>
        public RemoteCacheManager(ClientConfig configuration, Serializer s)
        {
            this.config = configuration;
            this.serializer = s;
            this.codec = new Codec();
            this.transportFactory = new TCPTransportFactory(this.config);
        }

        /// <summary>
        /// Constructor with default serializer
        /// </summary>
        /// <param name="configuration"></param>
        public RemoteCacheManager(ClientConfig configuration)
        {
            this.config = configuration;
            this.serializer = new DefaultSerializer();
            this.codec = new Codec();
            this.transportFactory = new TCPTransportFactory(this.config);
        }

        
        /// <summary>
        /// Cache with default settings mentioned in App.config file
        /// </summary>
         public RemoteCache getCache()
        {
            return new RemoteCacheImpl(this, this.config, this.serializer,this.transportFactory);
        }

        /// <summary>
        ///Cache with default settings and a given cacheName
         /// </summary>
        public RemoteCache getCache(String cacheName)
        {
            return new RemoteCacheImpl(this, this.config, cacheName, this.serializer,this.transportFactory);
        }

        
        /// <summary>
        /// Cache with specified forceRetunValue parameter
        /// </summary>
        /// <param name="forceRetunValue"></param>
        /// <returns></returns>
        public RemoteCache getCache(bool forceRetunValue)
        {
            return new RemoteCacheImpl(this, this.config, forceRetunValue, this.serializer,this.transportFactory);
        }

        
        /// <summary>
        ///Specified named cache with customized forceRetunValue option
        /// </summary>
        /// <param name="cacheName">If the user needs to give the cachename manually it can be passed here</param>
        /// <param name="forceRetunValue">If forceRetunValue is true, cache returns the value existed before the operation</param>
        /// <returns></returns>
        public RemoteCache getCache(String cacheName, bool forceRetunValue)
        {
            return new RemoteCacheImpl(this, this.config, forceRetunValue, this.serializer, this.transportFactory);
        }
    }
}

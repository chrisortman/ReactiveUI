﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReactiveXaml;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ReactiveXaml.Serialization
{
    public abstract class ModelBase : ReactiveValidatedObject
    {
        public Guid ContentHash { get; protected set; }

        public DateTimeOffset CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public ModelBase()
        {
            setupModelBase();
        }

        [OnDeserialized]
        void setupModelBase(StreamingContext sc) { setupModelBase(); }
        void setupModelBase()
        {
            this.Subscribe(_ => {
                UpdatedOn = DateTimeOffset.Now;
                ContentHash = CalculateHash();
            });
        }

        public virtual Guid CalculateHash()
        {
            var json = JSONHelper.Serialize(this);
            this.Log().Debug(json);
            return new Guid(json.MD5Hash());
        }

        [IgnoreDataMember]
        public IObservable<object> ItemChanging {
            get { return BeforeChange.Select(_ => this); }
        }

        [IgnoreDataMember]
        public IObservable<object> ItemChanged {
            get { return this.Select(_ => this); }
        }
    }
}

// vim: tw=120 ts=4 sw=4 et enc=utf8 :
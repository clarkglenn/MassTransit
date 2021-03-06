// Copyright 2007-2019 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the
// License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
namespace MassTransit.AutomatonymousAutofacIntegration.Registration
{
    using Autofac;
    using Automatonymous;
    using Automatonymous.Registration;


    public class AutofacSagaStateMachineRegistrar :
        ISagaStateMachineRegistrar
    {
        readonly ContainerBuilder _builder;

        public AutofacSagaStateMachineRegistrar(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public void RegisterStateMachineSaga<TStateMachine, TInstance>()
            where TStateMachine : class, SagaStateMachine<TInstance>
            where TInstance : class, SagaStateMachineInstance
        {
            _builder.RegisterType<AutofacSagaStateMachineFactory>()
                .As<ISagaStateMachineFactory>()
                .SingleInstance();

            _builder.RegisterType<AutofacStateMachineActivityFactory>()
                .As<IStateMachineActivityFactory>()
                .SingleInstance();

            _builder.RegisterType<TStateMachine>()
                .AsSelf()
                .As<SagaStateMachine<TInstance>>()
                .SingleInstance();
        }
    }
}

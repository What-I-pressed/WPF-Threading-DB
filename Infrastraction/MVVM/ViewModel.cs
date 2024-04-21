using Infrastraction.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Domain.Data.Entities;
using Microsoft.Extensions.Primitives;

namespace Infrastraction.MVVM
{
    public class ViewModel
    {
        Model _model;
        View _view;

        public ViewModel()
        {
            _model = new Model();
            _view = new View();
        }

        public void BindProgressBar(UserInsertItemDelegate del) => _model.BindInsertionEvent(del);

        public async void AddUsers(int count)
        {
            _view.InvokePBMaximumEvent(count);
            _view.StartTimer();
            await _model.AddRandomUsers(count);
            _view.InvokeDGUpdateEventAsync();            //асинхронний вивід
            _view.ShowTimerMeasurements();
        }

        public void BindPBMaximumEvent(Action<int> del) =>
            _view.BindPBMaximumEvent(del);

        public void BindMessageBoxEvent(Action<string> del) => _view.BindMessageBoxEvent(del);

        public void BindDGUpdateFunc(Action del) { _view.BindDGUpdateEvent(del); }

        public void InvokeDGUpdateEventAsync() => _view.InvokeDGUpdateEventAsync();

        public void EditUser(UserEntity e) { 
            _model.EditUser(e);
            _view.InvokeMessageBoxEvent("User was successfully changed");
        }
        public Task UpdateDGAsync() => _view.InvokeDGUpdateEventAsync();

        public void InvokeMessageBoxEvent(string var) => _view.InvokeMessageBoxEvent(var);
    }
}

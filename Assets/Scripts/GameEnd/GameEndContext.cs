﻿using _Shared;
using Battle;
using GameEnd.UI;
using Levels;

namespace GameEnd
{
    public class GameEndContext : ContextBehaviour<GameEndModel, GameEndView, GameEndPresenter>
    {
        private IContext<BattleModel> _battleContext;
        private ILevelManager _levelManager;

        public void Construct(IContext<BattleModel> battleContext, ILevelManager levelManager)
        {
            _battleContext = battleContext;
            _levelManager = levelManager;
        }

        private void Start()
        {
            BattleModel.PlayerWon += BattleModel_OnPlayerWon;
            BattleModel.PlayerLost += BattleModel_OnPlayerLost;
        }

        private BattleModel BattleModel => _battleContext.Model;

        private void BattleModel_OnPlayerWon() => Model.OnWon();
        private void BattleModel_OnPlayerLost() => Model.OnLost();


        protected override GameEndModel CreateModel() => new GameEndModel(_levelManager);

        protected override GameEndPresenter CreatePresenter(GameEndModel model, GameEndView view) =>
            new GameEndPresenter(model, view);

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            BattleModel.PlayerWon -= BattleModel_OnPlayerWon;
            BattleModel.PlayerLost -= BattleModel_OnPlayerLost;
        }
    }
}
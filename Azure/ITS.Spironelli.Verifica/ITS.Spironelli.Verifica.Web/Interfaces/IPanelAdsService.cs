﻿using ITS.Spironelli.Verifica.Domain.Entities;

namespace ITS.Spironelli.Verifica.Web.Interfaces;

public interface IPanelAdsService
{
    Task<bool> SetPanelMessage(int panelId, PanelMessage message);
    Task<bool> ChangePanelConfiguration(int panelId, PanelConfiguration configuration);
}
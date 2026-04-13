import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../featured/AuthAndPermissions/authSlice";
import adminPanelSettingReducer from "../featured/GeneralSettings/adminPanelSettingSlice";
import treasuryReducer from "../featured/GeneralSettings/treasurySlice";
import { authPersistMiddleware, loadAuthFromStorage } from "./authMiddleware";

const savedUser = loadAuthFromStorage();

export const store = configureStore({
  reducer: {
    auth: authReducer,
    adminPanelSetting: adminPanelSettingReducer,
    treasuries: treasuryReducer,
  },
  preloadedState: savedUser ? { auth: { user: savedUser, loading: false, error: null } } : undefined,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authPersistMiddleware),
});

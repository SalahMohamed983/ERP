import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../axiosInstance";

const BASE = "/genralsettings/admin/AdminPanelSetting";

const initialState = {
  item: null,
  loading: false,
  error: null,
};

export const getAdminPanelSetting = createAsyncThunk(
  "adminPanelSetting/get",
  async (id, { rejectWithValue }) => {
    try {
      const res = await api.get(`${BASE}/${id}`);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return data;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل تحميل الإعدادات");
    }
  }
);

export const createAdminPanelSetting = createAsyncThunk(
  "adminPanelSetting/create",
  async (dto, { rejectWithValue }) => {
    try {
      const res = await api.post(BASE, dto);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return data;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل إنشاء الإعدادات");
    }
  }
);

export const updateAdminPanelSetting = createAsyncThunk(
  "adminPanelSetting/update",
  async (dto, { rejectWithValue }) => {
    try {
      const res = await api.put(BASE, dto);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return data ?? dto;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل تحديث الإعدادات");
    }
  }
);

/**
 * رفع صورة الشعار إلى السيرفر. يُرجع المسار النسبي (path) لحفظه في الحقل photo.
 */
export const uploadAdminPanelPhoto = createAsyncThunk(
  "adminPanelSetting/uploadPhoto",
  async (file, { rejectWithValue }) => {
    try {
      const formData = new FormData();
      formData.append("file", file);
      const res = await api.post(`${BASE}/upload-photo`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      const path = res.data?.path ?? res.data?.Path;
      if (!path) return rejectWithValue("لم يُرجع السيرفر مسار الصورة.");
      return path;
    } catch (err) {
      const msg =
        err.response?.data?.message ??
        err.response?.data?.Message ??
        err.response?.data?.title ??
        "فشل رفع الصورة";
      return rejectWithValue(msg);
    }
  }
);

function getErrorMessage(payload, fallback) {
  if (payload == null) return fallback;
  if (typeof payload === "string") return payload;
  if (payload.message) return payload.message;
  if (payload.Message) return payload.Message;
  if (payload.title) return payload.title;
  if (payload.detail) return payload.detail;
  if (payload.errors && typeof payload.errors === "object") {
    const key = Object.keys(payload.errors)[0];
    const val = key ? payload.errors[key] : null;
    if (Array.isArray(val) && val[0]) return val[0];
    if (typeof val === "string") return val;
  }
  return fallback;
}

const adminPanelSettingSlice = createSlice({
  name: "adminPanelSetting",
  initialState,
  reducers: {
    clearAdminPanelSettingError(state) {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    // get
    builder
      .addCase(getAdminPanelSetting.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(getAdminPanelSetting.fulfilled, (state, action) => {
        state.loading = false;
        state.item = action.payload;
        state.error = null;
      })
      .addCase(getAdminPanelSetting.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(
          action.payload,
          "فشل تحميل إعدادات اللوحة"
        );
      });

    // create
    builder
      .addCase(createAdminPanelSetting.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(createAdminPanelSetting.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        if (action.payload != null && typeof action.payload === "object" && action.payload.id != null) {
          state.item = { ...state.item, ...action.payload };
        } else if (typeof action.payload === "number") {
          state.item = { ...state.item, id: action.payload };
        }
      })
      .addCase(createAdminPanelSetting.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(
          action.payload,
          "فشل إنشاء إعدادات اللوحة"
        );
      });

    // update
    builder
      .addCase(updateAdminPanelSetting.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateAdminPanelSetting.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        const updated =
          action.payload && typeof action.payload === "object" && Object.keys(action.payload).length > 0
            ? action.payload
            : action.meta.arg;
        state.item = { ...state.item, ...updated };
      })
      .addCase(updateAdminPanelSetting.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(
          action.payload,
          "فشل تحديث إعدادات اللوحة"
        );
      });
  },
});

export const { clearAdminPanelSettingError } = adminPanelSettingSlice.actions;
export default adminPanelSettingSlice.reducer;

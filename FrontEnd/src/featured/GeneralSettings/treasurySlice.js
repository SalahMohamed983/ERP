import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../axiosInstance";

const BASE = "/genralsettings/treasuries/Treasury";

const initialState = {
  items: [],
  loading: false,
  error: null,
};

export const getTreasuries = createAsyncThunk(
  "treasuries/getAll",
  async (_, { rejectWithValue }) => {
    try {
      const res = await api.get(BASE);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return Array.isArray(data) ? data : [];
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل تحميل الخزن");
    }
  }
);

export const createTreasury = createAsyncThunk(
  "treasuries/create",
  async (dto, { rejectWithValue, getState }) => {
    try {
      // const state = getState();
      // const userId = state.auth?.user?.id ?? state.auth?.user?.userId ?? 0;
      const payload = {
        name: dto.name || "",
        isMaster: !!dto.isMaster,
        active: dto.active !== false,
        lastIsalExhcange: dto.lastIsalExhcange ?? 0,
        lastIsalCollect: dto.lastIsalCollect ?? 0,
        // addedBy: userId,
      };
      const res = await api.post(BASE, payload);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return data;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل إضافة الخزنة");
    }
  }
);

export const updateTreasury = createAsyncThunk(
  "treasuries/update",
  async (dto, { rejectWithValue, getState }) => {
    try {
      const state = getState();
      const userId = state.auth?.user?.id ?? state.auth?.user?.userId ?? null;
      const payload = {
        id: dto.id,
        name: dto.name || "",
        isMaster: !!dto.isMaster,
        active: dto.active !== false,
        lastIsalExhcange: dto.lastIsalExhcange ?? 0,
        lastIsalCollect: dto.lastIsalCollect ?? 0,
        updatedBy: userId
      };
      const res = await api.put(BASE, payload);
      const data = res.data?.data ?? res.data?.Data ?? res.data;
      return data ?? payload;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل تحديث الخزنة");
    }
  }
);

export const deleteTreasury = createAsyncThunk(
  "treasuries/delete",
  async (id, { rejectWithValue }) => {
    try {
      await api.delete(`${BASE}/${id}`);
      return id;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل حذف الخزنة");
    }
  }
);

export const deleteTreasuries = createAsyncThunk(
  "treasuries/deleteMultiple",
  async (ids, { rejectWithValue }) => {
    try {
      await api.post(`${BASE}/delete-multiple`, ids);
      return ids;
    } catch (err) {
      return rejectWithValue(err.response?.data ?? "فشل حذف الخزن");
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

const treasurySlice = createSlice({
  name: "treasuries",
  initialState,
  reducers: {
    clearTreasuriesError(state) {
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getTreasuries.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(getTreasuries.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload ?? [];
        state.error = null;
      })
      .addCase(getTreasuries.rejected, (state, action) => {
        state.loading = false;
        state.items = [];
        state.error = getErrorMessage(action.payload, "فشل تحميل الخزن");
      });

    builder
      .addCase(createTreasury.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(createTreasury.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(createTreasury.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(action.payload, "فشل إضافة الخزنة");
      });

    builder
      .addCase(updateTreasury.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateTreasury.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        const updated = action.payload && typeof action.payload === "object" ? action.payload : action.meta.arg;
        const idx = state.items.findIndex((i) => i.id === updated.id);
        if (idx !== -1) state.items[idx] = { ...state.items[idx], ...updated };
      })
      .addCase(updateTreasury.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(action.payload, "فشل تحديث الخزنة");
      });

    builder
      .addCase(deleteTreasury.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(deleteTreasury.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        state.items = state.items.filter((i) => i.id !== action.payload);
      })
      .addCase(deleteTreasury.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(action.payload, "فشل حذف الخزنة");
      });

    builder
      .addCase(deleteTreasuries.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(deleteTreasuries.fulfilled, (state, action) => {
        state.loading = false;
        state.error = null;
        const ids = Array.isArray(action.payload) ? action.payload : [];
        state.items = state.items.filter((i) => !ids.includes(i.id));
      })
      .addCase(deleteTreasuries.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(action.payload, "فشل حذف الخزن");
      });
  },
});

export const { clearTreasuriesError } = treasurySlice.actions;
export default treasurySlice.reducer;


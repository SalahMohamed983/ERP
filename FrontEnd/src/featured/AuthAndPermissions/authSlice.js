import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import api from "../axiosInstance";

// الـ tokens الآن في HttpOnly cookies ولا يمكن الوصول إليها من JavaScript
// فقط نخزن بيانات المستخدم في Redux

/** استخراج رسالة خطأ قابلة للعرض من استجابة API (تجنب عرض كائن كامل في React) */
export function getErrorMessage(payload, fallback) {
  if (payload == null) return fallback;
  if (typeof payload === "string") return payload;
  if (payload.Message) return payload.Message;
  if (payload.title) return payload.title;
  if (payload.detail) return payload.detail;
  if (payload.errors && typeof payload.errors === "object") {
    const firstKey = Object.keys(payload.errors)[0];
    const firstVal = firstKey ? payload.errors[firstKey] : null;
    if (Array.isArray(firstVal) && firstVal[0]) return firstVal[0];
    if (typeof firstVal === "string") return firstVal;
  }
  return fallback;
}

const initialState = {
  user: null,
  loading: false,
  error: null,
};

export const login = createAsyncThunk(
  "auth/login",
  async ({ userName, password }, { rejectWithValue }) => {
    try {
      const res = await api.post("/Auth/login", {
        email:userName,
        password,
      });

      // الـ response يحتوي على User و ExpiresAt فقط (الـ tokens في cookies)
      return res.data?.data || res.data;
    } catch (err) {
      return rejectWithValue(
        err.response?.data || "Login failed"
      );
    }
  }
);

export const refreshToken = createAsyncThunk(
  "auth/refreshToken",
  async (_, { rejectWithValue }) => {
    try {
      const res = await api.post("/Auth/refresh-token", {});
      // الـ response يحتوي على User و ExpiresAt فقط (الـ tokens في cookies)
      return res.data?.Data || res.data;
    } catch (err) {
      return rejectWithValue(
        err.response?.data || "Refresh token failed"
      );
    }
  }
);

export const logout = createAsyncThunk(
  "auth/logout",
  async (_, { rejectWithValue }) => {
    try {
      await api.post("/Auth/logout");
      // الـ cookies سيتم حذفها من الباك إند
      return null;
    } catch (err) {
      return rejectWithValue(
        err.response?.data || "Logout failed"
      );
    }
  }
);

export const forgotPassword = createAsyncThunk(
  "auth/forgotPassword",
  async ({ email }, { rejectWithValue }) => {
    try {
      const res = await api.post("/Auth/forgot-password", {
        email,
      });
      return res.data;
    } catch (err) {
      return rejectWithValue(
        err.response?.data || "Forgot password failed"
      );
    }
  }
);

export const resetPassword = createAsyncThunk(
  "auth/resetPassword",
  async (
    { email, token, newPassword, confirmPassword },
    { rejectWithValue }
  ) => {
    try {
      const res = await api.post("/Auth/reset-password", {
        email,
        token,
        newPassword,
        confirmPassword,
      });
      return res.data;
    } catch (err) {
      return rejectWithValue(
        err.response?.data || "Reset password failed"
      );
    }
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    clearAuth(state) {
      state.user = null;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    // login
    builder
      .addCase(login.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false;
        // الـ tokens في HttpOnly cookies، فقط نخزن بيانات المستخدم
        state.user = action.payload?.User || action.payload?.user || null;
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(
          action.payload,
          "فشل تسجيل الدخول، يرجى المحاولة مرة أخرى."
        );
      });

    // refresh
    builder
      .addCase(refreshToken.fulfilled, (state, action) => {
        // تحديث بيانات المستخدم فقط
        state.user = action.payload?.User || action.payload?.user || state.user;
      })
      .addCase(refreshToken.rejected, (state) => {
        state.user = null;
      });

    // logout
    builder
      .addCase(logout.fulfilled, (state) => {
        state.user = null;
        state.error = null;
      })
      .addCase(logout.rejected, (state, action) => {
        // حتى لو فشل الـ logout، امسح الـ state
        state.user = null;
        state.error = getErrorMessage(action.payload, null);
      });

    // forgot password
 builder
      .addCase(forgotPassword.pending, (state) => {
        state.loading = true;
        state.error = null;
      }).
      addCase(forgotPassword.fulfilled, (state) => {
        state.loading = false;
      }).
      addCase(forgotPassword.rejected, (state, action) => {
  state.loading = false;
      state.error = getErrorMessage(
        action.payload,
        "حدث خطأ أثناء إرسال رابط استعادة كلمة المرور."
      );
    });

    // reset password
    builder
      .addCase(resetPassword.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(resetPassword.fulfilled, (state) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(resetPassword.rejected, (state, action) => {
        state.loading = false;
        state.error = getErrorMessage(
          action.payload,
          "فشل إعادة تعيين كلمة المرور، يرجى المحاولة مرة أخرى."
        );
      });
  },
});

export const { clearAuth } = authSlice.actions;
export default authSlice.reducer;


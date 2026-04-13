import axios from "axios";

const baseURL = import.meta.env.VITE_API_URL || "https://localhost:7012/api";
const api = axios.create({
  baseURL,
  withCredentials: true, // مهم لإرسال الـ cookies تلقائياً
});

/** أصل عنوان الـ API (بدون /api) لعرض الصور والملفات الثابتة من نفس السيرفر */
export const getApiOrigin = () => baseURL.replace(/\/api\/?$/, "") || baseURL;

// الـ cookies HttpOnly ستُرسل تلقائياً من المتصفح
// لا حاجة لإضافة Authorization header يدوياً لأن الباك إند سيقرأ الـ access token من cookie

let isRefreshing = false;
let failedQueue = [];

const processQueue = (error) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve();
    }
  });
  failedQueue = [];
};

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (
      error.response &&
      error.response.status === 401 &&
      !originalRequest._retry
    ) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        }).then(() => {
          return api(originalRequest);
        });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        // استدعاء refresh-token بدون body (الـ cookies تُرسل تلقائياً)
        await axios.post(
          `${api.defaults.baseURL}/Auth/refresh-token`,
          {},
          { withCredentials: true }
        );

        processQueue(null);
        isRefreshing = false;

        // إعادة المحاولة - الـ cookies الجديدة ستُرسل تلقائياً
        return api(originalRequest);
      } catch (err) {
        processQueue(err);
        isRefreshing = false;
        // في حالة فشل الـ refresh، يمكن توجيه المستخدم لصفحة تسجيل الدخول
        return Promise.reject(err);
      }
    }

    return Promise.reject(error);
  }
);

export default api;

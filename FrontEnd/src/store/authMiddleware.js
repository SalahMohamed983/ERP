// Middleware بسيط لحفظ وتحميل بيانات المستخدم من localStorage
export const authPersistMiddleware = (store) => (next) => (action) => {
  const result = next(action);

  // حفظ المستخدم في localStorage عندما يتغير auth state
  if (action.type?.includes('auth') || action.type?.includes('login') || action.type?.includes('logout')) {
    try {
      const state = store.getState();
      const user = state?.auth?.user;
      
      if (user) {
        localStorage.setItem('authUser', JSON.stringify(user));
      } else {
        localStorage.removeItem('authUser');
      }
    } catch (err) {
      console.warn('Failed to save auth data:', err);
    }
  }

  return result;
};

// دالة لتحميل المستخدم من localStorage
export const loadAuthFromStorage = () => {
  try {
    const raw = localStorage.getItem('authUser');
    if (!raw) return null;
    return JSON.parse(raw);
  } catch (err) {
    console.warn('Failed to load auth data:', err);
    return null;
  }
};

import { configureStore } from '@reduxjs/toolkit'
import { departmentReducer } from "./department/departmentSlice"
import { payrollReducer } from "./payroll/payrollSlice"

export const store = configureStore({
  reducer: {
    department: departmentReducer,
    payroll: payrollReducer
  }
})
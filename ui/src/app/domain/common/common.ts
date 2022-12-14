// import { HttpResponse } from "@angular/common/http"

export interface Page<T> {
    currentPage: number
    pageSize: number
    totalPages: number
    totalItems: number
    items: T[]
}

export interface ApplicationNotification {
    message: string
    notifications: string[]
}

export class HttpErrorResponse {
    correlationId: string | null | undefined
    status!: number
    message!: string
    notifications!: string[]

    constructor(httpResponse: any) {
        Object.assign(this, {
            correlationId: httpResponse.headers?.get('x-correlation-id'),
            status: httpResponse.status,
            message: httpResponse.error?.message,
            notifications: httpResponse.error?.notifications ?? []
        })
    }

    static set(httpResponse: any): HttpErrorResponse {
        return new HttpErrorResponse(httpResponse)
    }
}
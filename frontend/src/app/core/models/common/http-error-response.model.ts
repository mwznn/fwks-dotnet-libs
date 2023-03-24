export class HttpErrorResponse {
    correlationId: string | null | undefined;
    status!: number;
    message!: string;
    notifications!: string[];

    constructor(httpResponse: any) {
        Object.assign(this, {
            correlationId: httpResponse.headers?.get('x-correlation-id'),
            status: httpResponse.status,
            message: httpResponse.error?.message,
            notifications: httpResponse.error?.notifications ?? [],
        });
    }

    static set(httpResponse: any): HttpErrorResponse {
        return new HttpErrorResponse(httpResponse);
    }
}



  get{!FIELDS}ByFilter(filter: GeneralManagementFilterRequestModel)
    : Observable<APIResponse<APIPagedResponse<GeneralManagementFilterResponseModel[]>>> {
    const url = `${this.basePath}/{!AREA}/{!URLS}/get-filter-{!URLS}`;
    return this.http.post<APIResponse<APIPagedResponse<GeneralManagementFilterResponseModel[]>>>(url, filter);
  }
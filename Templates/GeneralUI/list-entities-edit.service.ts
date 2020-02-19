
  // {!FIELDS_EN}

  get{!FIELD}ById(entityId: number): Observable<APIResponse<GeneralManagementModel>> {
    const url = `${this.basePath}/{!AREA}/{!URLS}/${entityId}`;
    return this.http.get<APIResponse<GeneralManagementModel>>(url);
  }

  add{!FIELD}(entityModel: GeneralManagementModel): Observable<APIResponse<GeneralManagementModel>> {
    const url = `${this.basePath}/{!AREA}/{!URLS}`;
    return this.http.post<APIResponse<GeneralManagementModel>>(url, entityModel);
  }

  update{!FIELD}(entityModel: GeneralManagementModel): Observable<APIResponse<GeneralManagementModel>> {
    const url = `${this.basePath}/{!AREA}/{!URLS}/${entityModel.EntityId}`;
    return this.http.put<APIResponse<GeneralManagementModel>>(url, entityModel);
  }

  delete{!FIELD}(entityId: number): Observable<APIResponse<boolean>> {
    const url = `${this.basePath}/{!AREA}/{!URLS}/${entityId}`;
    return this.http.delete<APIResponse<boolean>>(url);
  }
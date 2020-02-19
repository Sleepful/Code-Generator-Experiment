  {
    path: 'manage-{!URLS}',
    component: GeneralListManagementComponent,
    canActivate: [AuthGuard],
    data: {
      module: ModulesEnum.{!MODULE_ENUM}, accessLevel: AccessLevelEnum.List,
      generalType: GeneralManagementEnum.{!FIELDS}
    }
  },
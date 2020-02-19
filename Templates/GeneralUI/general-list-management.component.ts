
      case GeneralManagementEnum.{!FIELDS}:
        this.hasManagementReadPermission = this.userSessionService
          .hasPermission(ModulesEnum.{!MODULE_ENUM}, AccessLevelEnum.Read);
        this.hasManagementAddEditPermission = this.userSessionService
          .hasPermission(ModulesEnum.{!MODULE_ENUM}, AccessLevelEnum.AddEdit);

        this.pageTitle = this.translatePipe.transform('shared-text', '{!FIELDS_CC}').value;
        this.pageLink = 'manage-{!URLS}';
        this.createNewEntityTitle = 'createNew{!FIELD}';
        this.breadCrumbSecond = '{!AREA_CC}SetUp';

        break;



        case GeneralManagementEnum.{!FIELDS}:
            operation = this.listEntitiesService.get{!FIELDS}ByFilter(entityFilter);
            break;
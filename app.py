import re
import os
import datetime
#Begin automation
print('Hello, metaprogramming!')
# {!AREA} {!AREA_CC} {!FIELD} {!FIELD_CC} {!FIELDS} {!FIELDS_CC} {!URLS}
area = 'Payroll'
field = 'PaymentFrequency'
fields = 'PaymentFrequencies'
urls = 'payment-frequencies'
moduleEnum = 'EmployeeSetup'
parentEnum = 'TaxesManagement'

#API: enum
#UI: module permissions, shared-text, edit.service
fields_en = 'Payment Frequencies' #basically the same as field but with spaces and case
fields_es = 'Frecuencias de Pago'
policyName = 'Payroll Full Access'

#UI: shared-text
field_en = 'Payment Frequency'
field_es = 'Frecuencia de Pago'

def main():
  initialize()
  # generalCrud()
  generalUI()

def generalUI():
  setGeneralUIPaths()
  fillAppRouting()
  fillLeftMenu()
  fillGeneralUI()
  fillTranslationsUI()
  print('\nremember to add new enum modules if necessary:')
  print('    > modify: modules.enum.ts')
  print('    '+moduleEnum+' = numbah')
  print('    > modify: general-management-type-enum.ts')
  print('    '+fields+' = numbah')

def fillTranslationsUI():
  fillTemplates('shared-text\en.js', 'shared-text\en.js',
    replaceContent)
  fillTemplates('shared-text\es.js', 'shared-text\es.js',
    replaceContent)
  fillTemplates('common-management\en.js', 'common-management\en.js',
    replaceContent)
  fillTemplates('common-management\es.js', 'common-management\es.js',
    replaceContent)


def fillGeneralUI():
  fillTemplates(
    'list-entities-edit.service.ts',
    'list-entities-edit.service.ts',
    replaceContent)
  fillTemplates(
    'list-entities.service.ts',
    'list-entities.service.ts',
    replaceContent)
  fillTemplates(
    'general-list-management.component.ts',
    'general-list-management.component.ts',
    replaceContent)
  fillTemplates(
    'general-list-management-edit.component.ts',
    'general-list-management-edit.component.ts',
    replaceContent)

def fillLeftMenu():
  fillTemplates(
    'left-menu.component.ts',
    'left-menu.component.ts',
    replaceContent)
  fillTemplates(
    'left-menu.component.html',
    'left-menu.component.html',
    replaceContent)

def fillAppRouting():
  fillTemplates(
    'app-routing.module.ts',
    'app-routing.module.ts',
    replaceContent)

def setGeneralUIPaths():
  global templatePath
  global outputPath
  templatePath = 'Templates\GeneralUI\\'
  outputPath = 'Output\GeneralUI\\'

def generalCrud():
  setGeneralCrudPaths()
  fillEntity()
  fillController()
  fillService()
  fillRetrieveByFilterSproc()
  fillDbContext()
  fillAutoMapping()
  fillModuleMigration()
  fillSprocMigration()
  
  print('\nremember to add migration for entity:')
  print('    * dotnet ef migrations add add-utb'+fields)
  #remember to add migration for sproc
  print('\nremember to add migration for sproc:')
  print('    * dotnet ef migrations add add-uspRetrieve'+fields+'ByFilter')
  #remmeber to add new enum modules if necessary
  print('\nremember to add new enum modules if necessary:')
  print('    > modify: ModuleEnum.cs')
  print('    [Description("'+fields_en+'")]')
  print('    '+moduleEnum+' = numbah')
  print('    * dotnet ef migrations add add-ModulePermissions'+fields)

  print('\nremember to add default values if any')
  print('\nUpdate :D')
  print('    * dotnet ef database update')


def setGeneralCrudPaths():
  global templatePath
  global outputPath
  templatePath = 'Templates\GeneralCrud\\'
  outputPath = 'Output\GeneralCrud\\'

# generate DatabaseContext stuff based on entity file?
def generateKeyNames():
  print('hehehe')

def replaceContent(data):
  for var, pattern in pairsToReplace:
    data = re.sub(re.escape(pattern), var, data)
  return data

def fillSprocMigration():
  fillTemplates(
    'SprocMigration.cs',
    'SprocMigration.cs',
    replaceContent)

def fillModuleMigration():
  fillTemplates(
    'ModuleMigration.cs',
    'ModuleMigration.cs',
    replaceContent)

def fillAutoMapping():
  fillTemplates(
    'AutoMapping.cs',
    'AutoMapping.cs',
    replaceContent)

def fillEntity():
  fillTemplates(
    'Entity.cs',
    field+'.cs',
    replaceContent)

def fillDbContext():
  fillTemplates(
    'DatabaseContext.cs',
    'DatabaseContext.cs',
    replaceContent)

def fillController():
  fillTemplates(
    'Controller.cs',
    field+'Controller.cs',
    replaceContent)

def fillService():
  fillTemplates(
    'Service.cs',
    field+'Service.cs',
    replaceContent)

def fillRetrieveByFilterSproc():
  fillTemplates(
    'uspRetrieveByFilter.sql',
    'uspRetrieve'+fields+'ByFilter.sql',
    replaceContent)

def fillTemplates(source, target, manipulation):
  data = '';
  with open(templatePath+source, 'r') as f:
    data = f.read()
  data = manipulation(data)
  os.makedirs(os.path.dirname(outputPath+target), exist_ok=True)
  with open(outputPath+target, 'w') as f:
    f.write(data)


def initialize():
  global pairsToReplace
  date = datetime.datetime.today().strftime('%d-%m-%Y')
  # service variables
  p_area = "{!AREA}"
  p_area_cc = "{!AREA_CC}"
  p_field = "{!FIELD}" #first with case
  p_fields = "{!FIELDS}" #plural, first with case
  p_fields_cc = "{!FIELDS_CC}" #plural, camel case
  p_field_cc = "{!FIELD_CC}" #camel case

  #for modules
  p_policyName = "{!POLICY}" #check db for this one
  p_field_es = "{!FIELD_ES}" #translation in spanish
  p_fields_es = "{!FIELDS_ES}" #translation in spanish
  p_field_es_lc = "{!FIELD_ES_LC}" #lowercase
  p_field_en = "{!FIELD_EN}" #translation in english
  p_fields_en = "{!FIELDS_EN}" #translation in english
  p_field_en_lc = "{!FIELD_EN_LC}" #lowercase

  # sproc var
  p_date = "{!DATE}" #current date for SPROCS

  # controller variables
  p_urls = "{!URLS}" #for example 'withholding-statuses'

  # check if ModuleEnum.{!FIELDS} exists
  # if not: generate migration file?

  p_moduleEnum = "{!MODULE_ENUM}"
  p_parentEnum = "{!OTHER_ENUM}" # routes accesible from other management places



  field_cc = field[:1].lower() + field[1:]
  fields_cc = fields[:1].lower() + fields[1:]
  area_cc = area[:1].lower() + area[1:]
  field_es_lc = field_es.lower()
  field_en_lc = field_en.lower()
  pairsToReplace = [(area, p_area), (field, p_field), (fields, p_fields),
              (urls, p_urls), (parentEnum, p_parentEnum), (moduleEnum, p_moduleEnum), (date, p_date), 
              (field_cc, p_field_cc), (policyName, p_policyName), 
              (fields_cc, p_fields_cc), (area_cc, p_area_cc),
              (field_es, p_field_es), (field_en, p_field_en), (fields_es, p_fields_es), (fields_en, p_fields_en),
              (field_es_lc, p_field_es_lc), (field_en_lc, p_field_en_lc)]

main()


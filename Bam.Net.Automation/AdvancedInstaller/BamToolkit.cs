﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.81.0.
// 
namespace Bam.Net.Automation.AdvancedInstaller {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class DOCUMENT {
        
        private DOCUMENTCOMPONENT[] cOMPONENTField;
        
        private string typeField;
        
        private string createVersionField;
        
        private string versionField;
        
        private string modulesField;
        
        private string rootPathField;
        
        private string languageField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("COMPONENT", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DOCUMENTCOMPONENT[] COMPONENT {
            get {
                return this.cOMPONENTField;
            }
            set {
                this.cOMPONENTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CreateVersion {
            get {
                return this.createVersionField;
            }
            set {
                this.createVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Modules {
            get {
                return this.modulesField;
            }
            set {
                this.modulesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string RootPath {
            get {
                return this.rootPathField;
            }
            set {
                this.rootPathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Language {
            get {
                return this.languageField;
            }
            set {
                this.languageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DOCUMENTCOMPONENT {
        
        private DOCUMENTCOMPONENTROW[] rOWField;
        
        private DOCUMENTCOMPONENTATTRIBUTE[] aTTRIBUTEField;
        
        private string cidField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ROW", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DOCUMENTCOMPONENTROW[] ROW {
            get {
                return this.rOWField;
            }
            set {
                this.rOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ATTRIBUTE", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DOCUMENTCOMPONENTATTRIBUTE[] ATTRIBUTE {
            get {
                return this.aTTRIBUTEField;
            }
            set {
                this.aTTRIBUTEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cid {
            get {
                return this.cidField;
            }
            set {
                this.cidField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DOCUMENTCOMPONENTROW {
        
        private string propertyField;
        
        private string valueField;
        
        private string valueLocIdField;
        
        private string typeField;
        
        private string multiBuildValueField;
        
        private string directoryField;
        
        private string directory_ParentField;
        
        private string defaultDirField;
        
        private string isPseudoRootField;
        
        private string componentField;
        
        private string componentIdField;
        
        private string directory_Field;
        
        private string attributesField;
        
        private string keyPathField;
        
        private string featureField;
        
        private string titleField;
        
        private string descriptionField;
        
        private string displayField;
        
        private string levelField;
        
        private string componentsField;
        
        private string fileField;
        
        private string component_Field;
        
        private string fileNameField;
        
        private string sourcePathField;
        
        private string selfRegField;
        
        private string nextFileField;
        
        private string digSignField;
        
        private string buildKeyField;
        
        private string buildNameField;
        
        private string buildOrderField;
        
        private string buildTypeField;
        
        private string languagesField;
        
        private string installationTypeField;
        
        private string useLargeSchemaField;
        
        private string msiPackageTypeField;
        
        private string pathField;
        
        private string fragmentField;
        
        private string nameField;
        
        private string dialog_Field;
        
        private string control_Field;
        
        private string eventField;
        
        private string argumentField;
        
        private string conditionField;
        
        private string orderingField;
        
        private string optionsField;
        
        private string actionField;
        
        private string targetField;
        
        private string sourceField;
        
        private string withoutSeqField;
        
        private string multiBuildTargetField;
        
        private string environmentField;
        
        private string indexField;
        
        private string sequenceField;
        
        private string descriptionLocIdField;
        
        private string isPredefinedField;
        
        private string buildsField;
        
        private string registryField;
        
        private string rootField;
        
        private string keyField;
        
        private string upgradeCodeField;
        
        private string versionMinField;
        
        private string versionMaxField;
        
        private string actionPropertyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Property {
            get {
                return this.propertyField;
            }
            set {
                this.propertyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ValueLocId {
            get {
                return this.valueLocIdField;
            }
            set {
                this.valueLocIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MultiBuildValue {
            get {
                return this.multiBuildValueField;
            }
            set {
                this.multiBuildValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Directory {
            get {
                return this.directoryField;
            }
            set {
                this.directoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Directory_Parent {
            get {
                return this.directory_ParentField;
            }
            set {
                this.directory_ParentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DefaultDir {
            get {
                return this.defaultDirField;
            }
            set {
                this.defaultDirField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsPseudoRoot {
            get {
                return this.isPseudoRootField;
            }
            set {
                this.isPseudoRootField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Component {
            get {
                return this.componentField;
            }
            set {
                this.componentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ComponentId {
            get {
                return this.componentIdField;
            }
            set {
                this.componentIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Directory_ {
            get {
                return this.directory_Field;
            }
            set {
                this.directory_Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Attributes {
            get {
                return this.attributesField;
            }
            set {
                this.attributesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string KeyPath {
            get {
                return this.keyPathField;
            }
            set {
                this.keyPathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Feature {
            get {
                return this.featureField;
            }
            set {
                this.featureField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Display {
            get {
                return this.displayField;
            }
            set {
                this.displayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Level {
            get {
                return this.levelField;
            }
            set {
                this.levelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Components {
            get {
                return this.componentsField;
            }
            set {
                this.componentsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string File {
            get {
                return this.fileField;
            }
            set {
                this.fileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Component_ {
            get {
                return this.component_Field;
            }
            set {
                this.component_Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SourcePath {
            get {
                return this.sourcePathField;
            }
            set {
                this.sourcePathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SelfReg {
            get {
                return this.selfRegField;
            }
            set {
                this.selfRegField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NextFile {
            get {
                return this.nextFileField;
            }
            set {
                this.nextFileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DigSign {
            get {
                return this.digSignField;
            }
            set {
                this.digSignField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildKey {
            get {
                return this.buildKeyField;
            }
            set {
                this.buildKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildName {
            get {
                return this.buildNameField;
            }
            set {
                this.buildNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildOrder {
            get {
                return this.buildOrderField;
            }
            set {
                this.buildOrderField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildType {
            get {
                return this.buildTypeField;
            }
            set {
                this.buildTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Languages {
            get {
                return this.languagesField;
            }
            set {
                this.languagesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InstallationType {
            get {
                return this.installationTypeField;
            }
            set {
                this.installationTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UseLargeSchema {
            get {
                return this.useLargeSchemaField;
            }
            set {
                this.useLargeSchemaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MsiPackageType {
            get {
                return this.msiPackageTypeField;
            }
            set {
                this.msiPackageTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path {
            get {
                return this.pathField;
            }
            set {
                this.pathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Fragment {
            get {
                return this.fragmentField;
            }
            set {
                this.fragmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Dialog_ {
            get {
                return this.dialog_Field;
            }
            set {
                this.dialog_Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Control_ {
            get {
                return this.control_Field;
            }
            set {
                this.control_Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Event {
            get {
                return this.eventField;
            }
            set {
                this.eventField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Argument {
            get {
                return this.argumentField;
            }
            set {
                this.argumentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Condition {
            get {
                return this.conditionField;
            }
            set {
                this.conditionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Ordering {
            get {
                return this.orderingField;
            }
            set {
                this.orderingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Options {
            get {
                return this.optionsField;
            }
            set {
                this.optionsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Action {
            get {
                return this.actionField;
            }
            set {
                this.actionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Target {
            get {
                return this.targetField;
            }
            set {
                this.targetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Source {
            get {
                return this.sourceField;
            }
            set {
                this.sourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WithoutSeq {
            get {
                return this.withoutSeqField;
            }
            set {
                this.withoutSeqField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MultiBuildTarget {
            get {
                return this.multiBuildTargetField;
            }
            set {
                this.multiBuildTargetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Environment {
            get {
                return this.environmentField;
            }
            set {
                this.environmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Index {
            get {
                return this.indexField;
            }
            set {
                this.indexField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sequence {
            get {
                return this.sequenceField;
            }
            set {
                this.sequenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DescriptionLocId {
            get {
                return this.descriptionLocIdField;
            }
            set {
                this.descriptionLocIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsPredefined {
            get {
                return this.isPredefinedField;
            }
            set {
                this.isPredefinedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Builds {
            get {
                return this.buildsField;
            }
            set {
                this.buildsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Registry {
            get {
                return this.registryField;
            }
            set {
                this.registryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Root {
            get {
                return this.rootField;
            }
            set {
                this.rootField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UpgradeCode {
            get {
                return this.upgradeCodeField;
            }
            set {
                this.upgradeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VersionMin {
            get {
                return this.versionMinField;
            }
            set {
                this.versionMinField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VersionMax {
            get {
                return this.versionMaxField;
            }
            set {
                this.versionMaxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ActionProperty {
            get {
                return this.actionPropertyField;
            }
            set {
                this.actionPropertyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class DOCUMENTCOMPONENTATTRIBUTE {
        
        private string nameField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class NewDataSet {
        
        private DOCUMENT[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DOCUMENT")]
        public DOCUMENT[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }
}

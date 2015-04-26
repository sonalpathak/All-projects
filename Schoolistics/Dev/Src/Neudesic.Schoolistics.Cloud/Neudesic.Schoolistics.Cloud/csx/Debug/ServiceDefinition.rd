<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Neudesic.Schoolistics.Cloud" generation="1" functional="0" release="0" Id="03bad5eb-4ca6-434c-8f04-f94d75d4c308" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Neudesic.Schoolistics.CloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Neudesic.Schoolistics.WebRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/LB:Neudesic.Schoolistics.WebRole:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/LB:Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Certificate|Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapCertificate|Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRole:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRole:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="Neudesic.Schoolistics.WebRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/MapNeudesic.Schoolistics.WebRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Neudesic.Schoolistics.WebRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapCertificate|Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRole:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapNeudesic.Schoolistics.WebRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Neudesic.Schoolistics.WebRole" generation="1" functional="0" release="0" software="C:\Users\NeuAdmin\Desktop\Schoolistics\Dev\Src\Neudesic.Schoolistics.Cloud\Neudesic.Schoolistics.Cloud\csx\Debug\roles\Neudesic.Schoolistics.WebRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="81" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/SW:Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Neudesic.Schoolistics.WebRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Neudesic.Schoolistics.WebRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="Neudesic.Schoolistics.WebRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="Neudesic.Schoolistics.WebRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Neudesic.Schoolistics.WebRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="521f5369-f0f2-40c9-a22d-65ffc6dc7152" ref="Microsoft.RedDog.Contract\ServiceContract\Neudesic.Schoolistics.CloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="26b1fbc9-c343-448f-b400-47b5952302f1" ref="Microsoft.RedDog.Contract\Interface\Neudesic.Schoolistics.WebRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="44271877-e5c3-44c1-8131-77a90a5325da" ref="Microsoft.RedDog.Contract\Interface\Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Neudesic.Schoolistics.Cloud/Neudesic.Schoolistics.CloudGroup/Neudesic.Schoolistics.WebRole:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
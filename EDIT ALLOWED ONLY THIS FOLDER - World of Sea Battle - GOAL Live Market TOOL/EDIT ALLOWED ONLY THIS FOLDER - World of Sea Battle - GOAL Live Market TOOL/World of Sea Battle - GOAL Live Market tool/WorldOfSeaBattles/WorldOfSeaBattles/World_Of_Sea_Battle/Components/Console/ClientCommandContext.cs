using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace World_Of_Sea_Battle.Components.Console
{
	// Token: 0x0200054D RID: 1357
	[NullableContext(1)]
	[Nullable(0)]
	public class ClientCommandContext : IEquatable<ClientCommandContext>
	{
		// Token: 0x06001EDD RID: 7901 RVA: 0x00003A7C File Offset: 0x00001C7C
		public ClientCommandContext()
		{
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x001157F9 File Offset: 0x001139F9
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ClientCommandContext);
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00115808 File Offset: 0x00113A08
		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ClientCommandContext");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000030FD File Offset: 0x000012FD
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			return false;
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00115854 File Offset: 0x00113A54
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ClientCommandContext left, ClientCommandContext right)
		{
			return !(left == right);
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00115860 File Offset: 0x00113A60
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ClientCommandContext left, ClientCommandContext right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00115874 File Offset: 0x00113A74
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract);
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00115886 File Offset: 0x00113A86
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ClientCommandContext);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00115894 File Offset: 0x00113A94
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ClientCommandContext other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract);
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00003A7C File Offset: 0x00001C7C
		[CompilerGenerated]
		protected ClientCommandContext(ClientCommandContext original)
		{
		}
	}
}

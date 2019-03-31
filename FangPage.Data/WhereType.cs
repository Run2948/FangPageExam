using System;

namespace FangPage.Data
{
	// Token: 0x02000019 RID: 25
	public enum WhereType
	{
		// Token: 0x04000043 RID: 67
		Equal,
		// Token: 0x04000044 RID: 68
		NotEqual,
		// Token: 0x04000045 RID: 69
		GreaterThan,
		// Token: 0x04000046 RID: 70
		GreaterThanEqual,
		// Token: 0x04000047 RID: 71
		LessThan,
		// Token: 0x04000048 RID: 72
		LessThanEqual,
		// Token: 0x04000049 RID: 73
		Like,
		// Token: 0x0400004A RID: 74
		NotLike,
		// Token: 0x0400004B RID: 75
		In,
		// Token: 0x0400004C RID: 76
		NotIn,
		// Token: 0x0400004D RID: 77
		Contain,
		// Token: 0x0400004E RID: 78
		ContainOrEmpty,
		// Token: 0x0400004F RID: 79
		NotContain,
		// Token: 0x04000050 RID: 80
		IsNull,
		// Token: 0x04000051 RID: 81
		IsNotNull,
		// Token: 0x04000052 RID: 82
		IsNullOrEmpty,
		// Token: 0x04000053 RID: 83
		IsEmpty,
		// Token: 0x04000054 RID: 84
		Custom
	}
}
